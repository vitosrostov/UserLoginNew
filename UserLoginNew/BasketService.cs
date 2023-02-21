namespace UserLoginNew
{
    public class BasketService : IBasketService, IBasketTempService
    {
        public int Tovars { get; set; }

        public BasketService()
        {
            Tovars = 0;
        }
        public void AddAnyTovars(int anytovars)
        { Tovars += anytovars; }
        public void RemoveAnyTovars(int anytovars)
        { Tovars -= anytovars; }
    }
}
