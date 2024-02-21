using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone_Market.Code;
using Phone_Market.DTO;
using Phone_Market.Models;
using Phone_Market.Services;
using Stripe.Checkout;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MimeKit;
using MimeKit.Text;
using MimeKit.IO;
using MailKit.Net.Smtp;
using System;
using Phone_Market.Services.EmailImplementation;

namespace Phone_Market.Controllers
{
    public class ProductController : BaseController
    {


        public Product_Service product_Service;
        public EmailService EmailService;
        public Account_Service Account_Service;
        

        public ProductController(ControllerDependencies dependencies, Product_Service product_Service,EmailService emailService,Account_Service account_Service) : base(dependencies) {
            this.product_Service = product_Service;
            this.EmailService = emailService;
            this.Account_Service = account_Service;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new ProductCreateModel();

            return View("Create_Product",product);
        }


        [HttpPost]
        public IActionResult Create(ProductCreateModel product)
        {
            product_Service.CreateProduct(product);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult AddFavouriteProduct(Guid productId, Guid userId)
        {
            if (userId == CurrentUser.Id)
            {
                product_Service.AddFavouriteProduct(productId, userId);
            }
            return Ok();

        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult RemoveFavouriteProduct(Guid productId, Guid userId)
        {
            if (userId == CurrentUser.Id)
            {
                product_Service.RemoveFavouriteProduct(productId, userId);
            }
            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult IsFavouriteProduct(Guid userId, Guid productId)
        {
            var isfavouriteEvent = product_Service.GetFavouriteProducts(userId, productId);
            return Ok(isfavouriteEvent);
        }


        [HttpGet]
        public IActionResult ProductDetails(Guid id)
        {
            var product = product_Service.GetProductWithDetails(id);
            return View("ProductDetails", product);
        }

        
        [HttpGet]
        public IActionResult Cart(Guid userid )
        {
            var cart = product_Service.GetCart(userid);
            return View("Cart", cart);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid productId)
        {

            Guid userId = CurrentUser.Id;
            product_Service.AddToCart(productId, userId);
            return Ok();
            
        }

        [HttpPost]
        public IActionResult RemoveItem(Guid productId)
        {
            Guid userId = CurrentUser.Id;
            product_Service.RemoveItem(productId, userId);
            return Ok();
        }




        public IActionResult CheckOut()
        {

            var userid = CurrentUser.Id;
            
            var domain = "https://localhost:7180/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Product/OrderConfirmation",
                CancelUrl = domain + $"Product/Cart/{userid}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",

            };

            var cart = product_Service.GetCart(userid);
            foreach (var item in cart)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {

                        UnitAmount = (long?)(item.ProductPrice - item.ProductPrice * item.ProductDiscount / 100) * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{item.ProductName}"

                        },
                    },
                    Quantity = 1,
                };

                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }


        public IActionResult OrderConfirmation()
        {

            var userid = CurrentUser.Id;
            var email = Account_Service.GetUserMailById(userid);
            var cart = product_Service.GetCart(userid);
            var receipt = new Receipt();
            receipt.TotalPrice = product_Service.GetTotalPrice(userid);
            receipt.UserId = userid;
            product_Service.AddReceipt(receipt);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            PdfWriter.GetInstance(document, new FileStream(path + "/Receipt.pdf", FileMode.Create));
            document.Open();


            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK);
            var headingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);


            var table = new PdfPTable(3);
            table.WidthPercentage = 100;


            var titleCell = new PdfPCell(new Phrase("Your Order Receipt", titleFont));
            titleCell.Colspan = 3;
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.Border = 0;
            table.AddCell(titleCell);


            var headers = new string[] { "Product", "Price", "Quantity" };
            foreach (var header in headers)
            {
                var headerCell = new PdfPCell(new Phrase(header, headingFont));
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(headerCell);
            }

            foreach (var item in cart)
            {
                var product = product_Service.GetProductById(item.ProductId);


                table.AddCell(new PdfPCell(new Phrase(product.Name, normalFont)));
                table.AddCell(new PdfPCell(new Phrase("$" + (product.Price - product.Price * product.Discount / 100) * 1, normalFont)));
                table.AddCell(new PdfPCell(new Phrase("1", normalFont)));

                var OrderedItem = new OrderedItem();
                OrderedItem.ProductId = item.ProductId;
                OrderedItem.OrderId = receipt.Id;
                OrderedItem.Quantity = 1;

                product_Service.AddOrderedItem(OrderedItem, receipt.Id);
                product_Service.RemoveItemFromCart(item.ProductId,userid);
            }


            var totalCell = new PdfPCell(new Phrase("Total Price: $" + receipt.TotalPrice, headingFont));
            totalCell.Colspan = 4;
            totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalCell.Border = 0;
            table.AddCell(totalCell);


            document.Add(table);

            document.Close();
            _ = EmailService.SendDocument(email, "Order Receipt", "Thank you for your order", path + "/Receipt.pdf");
            return View("OrderConfirmation", cart);
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            
            try
            {
                var model = product_Service.GetEditProductDto(id);
                ViewBag.ProductId = id;
                return View("EditProduct", model);
            }
            catch (System.Exception)
            {
                return View("Error_NotFound");
            }
        }

        [HttpPost]
        public IActionResult EditProduct(EditProductDto model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            product_Service.EditProduct(model);
            return RedirectToAction("ProductDetails", "Product", new { id = model.Id });
        }


        public IActionResult DeleteProduct(Guid id)
        {
          
            try
            {
                product_Service.DeleteProduct(id);
                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception)
            {
                return View("Error_NotFound");
            }
        }
    }
}
