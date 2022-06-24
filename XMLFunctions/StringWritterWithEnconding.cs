using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLFunctions
{
    public class StringWritterWithEnconding : StringWriter
    {
        public StringWritterWithEnconding(Encoding encoding) : base()
        {
            this.m_Encoding = encoding;

        }

        private readonly Encoding m_Encoding;
        public override Encoding Encoding
        {
            get { return m_Encoding; }
        } 
    }
}
