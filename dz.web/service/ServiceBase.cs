using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dz.web.model;
using dz.web.Html;
namespace dz.web.service
{
    public class ServiceBase : IService
    {
        public virtual bool Insert(ModelBase model)
        {
            return DBUtility.DbHelperSQL.ExecuteSql(model.GetInsertSQL(), model.GetParameters()) > 0;
        }

        public virtual bool Update(ModelBase model)
        {
            return DBUtility.DbHelperSQL.ExecuteSql(model.GetUpdateSQL(), model.GetParameters()) > 0;
        }

        public virtual bool Delete(ModelBase model)
        {
            if (model == null) return false;
            return DBUtility.DbHelperSQL.ExecuteSql(model.GetDeleteSQL(), model.GetParameters()) > 0;
        }

        public virtual ModelBase GetModel(string where)        
        {
            var model = this.CreateTemplate();

            if (model == null) return null;

            string sql = model.GetSelectSQL(1);

            if (!string.IsNullOrWhiteSpace(where))
            {
                sql +=" where " + where;
            }

            var dt = DBUtility.DbHelperSQL.Query(sql);

            if (dt.Tables.Count > 0)
            {
               return TableToModelList(dt.Tables[0], model).FirstOrDefault();
            }
            return null;
        }

        #region 通用方法

        public virtual string GetServiceName()
        {
            return this.GetType().Name.Replace("Service", "");
        }

       
        public virtual ModelBase CreateTemplate()
        {
            foreach (System.Reflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {

                if (assembly.GetName().Name == "Anonymously Hosted DynamicMethods Assembly") continue;

                Type serviceType = assembly.GetTypes().FirstOrDefault(m => m.Name == (this.GetServiceName()));
                if (serviceType != null)
                {
                    return assembly.CreateInstance(serviceType.FullName) as ModelBase;
                }

            }
            return null;
        }

        public virtual ModelBase RowToModel(System.Data.DataRow dr, ModelBase template)
        {
            var propertys = template.GetType().GetProperties();
            foreach (var p in propertys)
            {
                if (dr.Table.Columns.Contains(p.Name) && dr[p.Name] != null)
                {
                    p.SetValue(template, dr[p.Name], null);
                }
            }
            return template.Clone() as ModelBase;
        }
        public virtual List<ModelBase> TableToModelList(System.Data.DataTable table, ModelBase template)
        {
            List<ModelBase> list = new List<ModelBase>();
            foreach (System.Data.DataRow dr in table.Rows)
            {
                list.Add(RowToModel(dr, template));
            }
            return list;
        }
        #endregion


        public virtual IEnumerable<ModelBase> GetModelList(string where, string order, int top)
        {
            List<ModelBase> list = new List<ModelBase>();


            return list;
        }

        public virtual TableListed GetModelList(string where, string order, int page, int pagesize)
        {
            TableListed tList = new TableListed();
            return tList;
        }

        public virtual void ResigterAction()
        {

        }
    }
}
