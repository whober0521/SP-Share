﻿using SP_Share.Services;
using SP_Share.Models;
using System.Web.Mvc;
using System.Web;
using System;

namespace SP_Share.Controllers
{
    public class ItemController : Controller
    {
        ItemService itemSrv;

        public ItemController()
        {
            itemSrv = new ItemService();
        }

        public ActionResult Index()
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Index", "Default");
            else
                return View(itemSrv.GetItemList(Session["UserAccount"].ToString(), Session["IsAdmin"].ToString()));
        }

        public ActionResult Put()
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Index", "Default");

            return View("Item", new Item());
        }

        public ActionResult Edit(Guid? idx)
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Index", "Default");

            if (idx == null)
                return RedirectToAction("Index");
            else
                return View("Item", itemSrv.GetItem(idx));
        }

        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Item item, HttpPostedFileBase contentfile)
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Index", "Default");

            TempData["ErrMsg"] = itemSrv.Save(item, contentfile.InputStream, contentfile.FileName, Session["UserAccount"].ToString());

            return RedirectToAction("Index");
        }

        public FileResult Download(Guid? idx)
        {
            Item item = itemSrv.GetItem(idx);

            return File(item.Content, System.Net.Mime.MediaTypeNames.Application.Octet, item.Name);
        }

        public ActionResult Delete(Guid? idx)
        {
            itemSrv.Delete(idx);

            return RedirectToAction("Index");
        }

        public ActionResult ItemLimitList()
        {
            return View(itemSrv.GetItemLimitList());
        }

        public ActionResult InsertLimit()
        {
            if (Session["IsAdmin"] == null || Session["IsAdmin"].ToString() != "True")
                return RedirectToAction("Index", "Default");

            return View("ItemLimit", new ItemLimit());
        }

        public ActionResult ItemLimit(string ext)
        {
            if (Session["IsAdmin"] == null || Session["IsAdmin"].ToString() != "True")
                return RedirectToAction("Index", "Default");

            return View(itemSrv.GetItemLimit(ext));
        }

        [ValidateAntiForgeryToken]
        public ActionResult SaveLimit(ItemLimit _limit)
        {
            if (Session["IsAdmin"] == null || Session["IsAdmin"].ToString() != "True")
                return RedirectToAction("Index", "Default");

            itemSrv.SaveLimit(_limit);

            return RedirectToAction("ItemLimitList");
        }

        public ActionResult DeleteLimit(string ext)
        {
            itemSrv.DeleteLimit(ext);

            return RedirectToAction("ItemLimitList");
        }
    }
}