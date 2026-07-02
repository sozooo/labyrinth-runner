namespace Application.Gameplay.Collectibles
{
    public readonly struct DiamondsSpawnedMessage
    {
        public readonly int TotalCount { get; }
        public DiamondsSpawnedMessage(int totalCount) => TotalCount = totalCount;
    }
}

