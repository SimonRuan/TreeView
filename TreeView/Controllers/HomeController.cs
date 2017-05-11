using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeView.DAL;
using TreeView.Models;

namespace TreeView.Controllers
{
    public class HomeController : Controller
    {
        private static GroupDAL groupDal = new GroupDAL();

        public ActionResult Index()
        {
            ViewBag.MaxLevel = groupDal.GetMaxLevel(); //获取最大层级，来确定“上级班组”下拉框的个数
            List<Node> model = groupDal.GetAllNodes().Where(x=>x.Level==0).ToList();
            return View(model);
        }

        /// <summary>
        /// 根据父Id获取nodes
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<Node> GetNodesByParentId(int parentId)
        {
            return groupDal.GetAllNodes().Where(x => x.ParentId == parentId).ToList();
        }

        /// <summary>
        /// 新增班组
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddGroup(Node node)
        {
            node.CreateTime = node.ModifyTime = DateTime.Now;
            var res = groupDal.AddGroup(node);
            if (res > 0)
            {
                return Json(new { flag = "1", content = "成功新增班组！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { flag = "0", content = "新增班组失败！" }, JsonRequestBehavior.AllowGet);

        }
    }
}
