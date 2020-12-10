using System;
using System.Collections.Generic;
using System.Text;
using static FactoryConsole.Factory.EnumHandler;

namespace FactoryConsole.Factory
{
    public class HandlerFactory
    {
        public AbstractHandler getHandler(EType pointEarningType)
        {
            switch (pointEarningType)
            {
                case EType.Basic:
                    return new Basic();
                case EType.Other:
                    return new Other();
                
                default:
                    return null;
            }
        }
    }

    public class DoTry
    {
        public void Do() 
        {
            int a = 0;
            EType etype = EType.DoNothing;
            switch (a)
            {
                case 1:   //Basic
                    etype = EType.Basic;
                    break;
                case 2:   //other
                    etype = EType.Other;
                    break;
                default:
                    break;
            }

            HandlerFactory handlerFactory = new HandlerFactory();
            AbstractHandler handler = null;
            handler = handlerFactory.getHandler(etype);
        }
    }
}
