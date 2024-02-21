using Phone_Market.Models;

namespace Phone_Market
{
    public class UnitOfWork
    {

        private readonly Phone_MarketContext Context;

        public UnitOfWork(Phone_MarketContext context)
        {
            this.Context = context;
        }


        private IRepository<Brand> brands;
        public IRepository<Brand> Brands => brands ??= new BaseRepository<Brand>(Context);

        private IRepository<Category> categories;
        public IRepository<Category> Categories => categories ??= new BaseRepository<Category>(Context);


        public IRepository<Color> colors;
        public IRepository<Color> Colors => colors ??= new BaseRepository<Color>(Context);

        public IRepository<Image> images;
        public IRepository<Image> Images => images ??= new BaseRepository<Image>(Context);

        public IRepository<OrderedItem> orderedItems;
        public IRepository<OrderedItem> OrderedItems => orderedItems ??= new BaseRepository<OrderedItem>(Context);

        public IRepository<Product> products;
        public IRepository<Product> Products => products ??= new BaseRepository<Product>(Context);

        public IRepository<Receipt> receipts;
        public IRepository<Receipt> Receipts => receipts ??= new BaseRepository<Receipt>(Context);

        public IRepository<Role> roles;
        public IRepository<Role> Roles => roles ??= new BaseRepository<Role>(Context);

        public IRepository<ShoppingCart> shoppingCarts;
        public IRepository<ShoppingCart> ShoppingCarts => shoppingCarts ??= new BaseRepository<ShoppingCart>(Context);

        public IRepository<User> users;
        public IRepository<User> Users => users ??= new BaseRepository<User>(Context);

        public IRepository<ProductRating> productRatings;
        public IRepository<ProductRating> ProductRatings => productRatings ??= new BaseRepository<ProductRating>(Context);


        public void SaveChanges()
        {
            Context.SaveChanges();
        }


    }
}
