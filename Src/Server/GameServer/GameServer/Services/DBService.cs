using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace GameServer.Services
{
    class DBService : Singleton<DBService>
    {
        ExtremeWorldEntities entities;

        public ExtremeWorldEntities Entities
        {
            get { return this.entities; }
        }

        public void Init()
        {
            entities = new ExtremeWorldEntities();
        }

        //float time = 0;
        /*public void Save()
        {
            //DateTime.Now.Ticks - time > xxxx
            entities.SaveChangesAsync();
        }*/

        public void Save(bool async = false)
        {
            if (async)
                entities.SaveChangesAsync();
            else
                entities.SaveChanges();
        }
    }
}
