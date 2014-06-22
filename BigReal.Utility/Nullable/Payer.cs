using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HPWorkUtility
{
    [TestFixture]
    public class PayerTests
    {
        #region INullable接口测试

        [Test]
        public void InstanceOfPayerWillBeNotNull()
        {
            var payer = new Payer();
            Assert.False(payer.IsNull());
        }

        [Test]
        public void InstanceOfNullPayerWillBeNull()
        {
            var payer = new NullPayer();
            Assert.True(payer.IsNull());
        }

        #endregion

        [Test]
        public void EmptyIsInstancOfNullPayer()
        {
            var payer = Payer.Empty;
            Assert.IsInstanceOf<NullPayer>(payer);
        }

        [Test]
        public void UserEmptyForPayDoNothing()
        {
            var payer = DbMethodGetEmptyPayer();
            payer.Pay(100);

            payer = DbMethodGetNotNullPayer();
            payer.Pay(100);
        }

        private Payer DbMethodGetEmptyPayer()
        {
            return Payer.Empty;
        }

        private Payer DbMethodGetNotNullPayer()
        {
            return new Payer();
        }


        //public void OrigenCode()
        //{
        //    var pay = new Payer();
        //    if (pay != null)
        //    {
        //        pay.Pay(150);
        //    }
        //}
    }
    public class Payer : INullable
    {
        #region 实现Null Object 模式

        private static readonly Payer _Empty = new NullPayer();
        public static Payer Empty
        {
            get
            {
                return _Empty;
            }
        }

        public static Payer NewNull()
        {
            return new NullPayer();
        }

        #endregion

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="money"></param>
        public virtual void Pay(decimal money)
        {
            // do pay
            Console.WriteLine(string.Format("支付：{0}元！", money));
        }

        public virtual bool IsNull()
        {
            return false;
        }
    }
}
