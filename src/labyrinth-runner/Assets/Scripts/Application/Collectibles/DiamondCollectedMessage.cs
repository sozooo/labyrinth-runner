namespace Application.Collectibles
{
    public readonly struct DiamondCollectedMessage
    {
        public readonly Diamond Diamond { get; }
        public DiamondCollectedMessage(Diamond diamond) => Diamond = diamond;
    }
}
