using NUnit.Framework;

namespace PM.Web.Tests
{
    [SetUpFixture]
    public class TestSetup
    {
        public void InitializeOneTimeData()
        {
            AutoMapperConfig.Initialize();
        }

        public void TearDown()
        {
            AutoMapper.Mapper.Reset();
        }
    }
}