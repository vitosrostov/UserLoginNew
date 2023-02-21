namespace UserLoginNew
{
    public interface IBasketService
    {
        int Tovars { get; set; }
        public void AddAnyTovars(int anytovars);
        public void RemoveAnyTovars(int anytovars);
    }
}
