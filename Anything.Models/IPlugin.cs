﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anything.Models
{
    [InheritedExport(typeof(IPlugin))]
    public interface IPlugin
    {

    }
}