﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Communication.Proxy;
using Infrastructure.Contract.Service;
using Microsoft.Practices.Unity;

namespace Client
{
    public class MyUnityContainer
    {
        public static readonly UnityContainer Instance = new UnityContainer();

        static MyUnityContainer()
        {
            // Proxies
            Instance.RegisterType<ITerminalsService, TerminalServiceProxy>(new ContainerControlledLifetimeManager());
        }
    }
}
