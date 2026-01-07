using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntakeSysteemBack.Interfaces;

namespace IntakeSysteemBack.Logic
{
    public class TemplateLogic
    {
        readonly ITemplate _Itemplate;

        //REMEMBER TO ADD STARTUP IF YOU ARE COPYING ME!!!

        public TemplateLogic(ITemplate itemp)
        {
            _Itemplate = itemp;
        }

        public string createFolder(string parent, string folderName)
        {
            return _Itemplate.createFolder(parent, folderName);
        }

    }
}
