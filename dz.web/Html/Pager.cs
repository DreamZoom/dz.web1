using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace dz.web.Html
{
    public class Pager
    {

        public Pager(TableListed tableListed)
        {
            this.PageIndex = tableListed.PageIndex;
            this.RecordCount = tableListed.RecordCount;
            this.PageSize = tableListed.PageSize;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return RecordCount / PageSize + (RecordCount % PageSize == 0 ? 0 : 1);
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }


        /// <summary>
        /// 最大 数字分页链接个数
        /// </summary>
        public int NumberLinkCount { get; set; }


        /// <summary>
        /// 当前页变量名
        /// </summary>
        public string ParamName { set; get; }


        private string getPageUrl(string url,string paramName,int page)
        {
            if (!url.Contains("?")) url += "?";
            url += "&" + paramName + "=" + page;
            return url;
        }

        /// <summary>
        /// 呈现分页条
        /// </summary>
        /// <returns></returns>
        public string Reader(string url)
        {

            List<string> listNumber = new List<string>();

            //first
            listNumber.Add(string.Format("<a href=\"{0}\">首页</a>", getPageUrl(url, ParamName, 1)));
            //prev
            listNumber.Add(string.Format("<a href=\"{0}\">上一页</a>", getPageUrl(url, ParamName, PageIndex - 1)));

            #region Number Links
            int loopcount = PageCount > NumberLinkCount ? NumberLinkCount : PageCount;

            int startpage = 1;

            if (PageCount > NumberLinkCount)
            {
                if (PageIndex > 3 && PageIndex < PageCount - 3) startpage = 1;
                if (PageIndex >= PageCount - 3) startpage = PageCount - loopcount;
            }

            string currentClass = "";
            for (int i = startpage; i < startpage + loopcount; i++)
            {
                if (i == PageIndex) currentClass = "active";
                listNumber.Add(string.Format("<a href=\"{0}\" class=\"{2}\">{1}</a>", getPageUrl(url, ParamName, i), i, currentClass));
            }

            #endregion

            //next
            listNumber.Add(string.Format("<a href=\"{0}\">下一页</a>", getPageUrl(url, ParamName, PageIndex + 1)));
            //prev
            listNumber.Add(string.Format("<a href=\"{0}\">尾页</a>", getPageUrl(url, ParamName, PageCount)));
            return string.Join("", listNumber.ToArray());
        }
    }

    public static class PagerExistions
    {
        public static MvcHtmlString Pager(this HtmlHelper helper,TableListed tableListed)
        {
            return new MvcHtmlString("");
        }
    }
}
