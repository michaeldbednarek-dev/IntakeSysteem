using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntakeSysteemBack.Models;
using IntakeSysteemBack.DAL;

namespace IntakeSysteemBack.Interfaces
{
    public interface ITemplate
    {
        string createFolder(string parent, string folderName);
    }
}
