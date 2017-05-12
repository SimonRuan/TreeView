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
            List<Node> model = groupDal.GetAllGroups().Where(x => x.level == 0).ToList();
            return View(model);
        }

        /// <summary>
        /// 根据父Id获取nodes
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGroupsByParentId(int parentId)
        {
            var res = groupDal.GetAllGroups().Where(x => x.parentId == parentId).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增班组
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddGroup(Node node)
        {
            node.createTime = node.modifyTime = DateTime.Now;
            var res = groupDal.AddGroup(node);
            if (res > 0)
            {
                return Json(new { flag = "1", content = "成功新增班组！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { flag = "0", content = "新增班组失败！" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取树状视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetTreeView()
        {
            var allGroups = groupDal.GetAllGroups();
            var maxLevel = allGroups.Select(x => x.level).Max();
            if (maxLevel == 0) return Json(allGroups, JsonRequestBehavior.AllowGet);
            var res = new List<Node>();
            for (int i = maxLevel - 1; i > -1; i--)
            {
                foreach (var item in allGroups.Where(x => x.level == i))
                {
                    item.nodes = allGroups.Any(x => x.parentId == item.id) ? allGroups.Where(x => x.parentId == item.id).ToList():null;
                }
            }
            res = allGroups.Where(x => x.level == 0).ToList();
            return Json(res, JsonRequestBehavior.AllowGet); ;
        }

        private void SetTreeView()
        {

        }
    }
}
