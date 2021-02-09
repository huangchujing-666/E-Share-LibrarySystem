using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data
{
    public class DataBaseInitializer : IDatabaseInitializer<LibrarySystemDbContext>
    {
        /// <summary>
        /// 数据库访问的初始化
        /// </summary>
        /// <param name="context">用户自定义DbContext类</param>
        public void InitializeDatabase(LibrarySystemDbContext context)
        {
            context.Database.CreateIfNotExists();
        }
    }
}
