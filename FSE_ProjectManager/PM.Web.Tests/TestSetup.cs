using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
