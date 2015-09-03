using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Logging.TestContext.Test
{
    public class TestContext : Microsoft.VisualStudio.TestTools.UnitTesting.TestContext
    {
        public List<string> _writeBuffer = new List<string>();


        public override System.Data.Common.DbConnection DataConnection
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override System.Data.DataRow DataRow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IDictionary Properties
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void AddResultFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public override void BeginTimer(string timerName)
        {
            throw new NotImplementedException();
        }

        public override void EndTimer(string timerName)
        {
            throw new NotImplementedException();
        }

        public override void WriteLine(string format, params object[] args)
        {
            _writeBuffer.Add(string.Format(format, args));
        }
    }
}
