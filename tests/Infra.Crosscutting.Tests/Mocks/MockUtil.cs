namespace Ritter.Infra.Crosscutting.Tests.Mocks
{
    internal class MockUtil
    {
        protected MockUtil()
        {
        }

        public static int GetPageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
                return 0;

            var remainder = totalCount % pageSize;
            return totalCount / pageSize + (remainder == 0 ? 0 : 1);
        }
    }
}
