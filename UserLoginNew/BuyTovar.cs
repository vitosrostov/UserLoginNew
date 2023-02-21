namespace UserLoginNew
{
    public class BuyTovar
    {
        public bool TovarBoolBuy { get; set; }
        public BuyTovar()
        {
            TovarBoolBuy = false;
        }
        public void BoolBuy()
        { TovarBoolBuy = true; }        
    }
}
