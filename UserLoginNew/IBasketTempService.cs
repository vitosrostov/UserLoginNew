namespace UserLoginNew
{
    public interface IBasketTempService
    {
        int Tovars { get; set; }
        public void AddAnyTovars(int anytovars);
        public void RemoveAnyTovars(int anytovars);
    }
}
