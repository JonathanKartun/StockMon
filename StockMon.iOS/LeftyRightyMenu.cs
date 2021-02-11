//using System;
//namespace StockMon.iOS
//{
//    public class LeftyRightyMenu
//    {
//        public LeftyRightyMenu()
//        {
//        }
//    }
//}


using System.Collections.Generic;
using StockMon.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(LeftyRightyMenuRenderer))]

//public class LeftyRightyMenuRenderer : PageRenderer
//{
//    public new ContentPage Element
//    {
//        get { return (ContentPage)base.Element; }
//    }

//    public override void ViewWillAppear(bool animated)
//    {
//        base.ViewWillAppear(animated);

//        var LeftNavList = new List<UIBarButtonItem>();
//        var rightNavList = new List<UIBarButtonItem>();

//        var navigationItem = this.NavigationController.TopViewController.NavigationItem;

//        for (var i = 0; i < Element.ToolbarItems.Count; i++)
//        {

//            var reorder = (Element.ToolbarItems.Count - 1);
//            var ItemPriority = Element.ToolbarItems[reorder - i].Priority;

//            if (ItemPriority == 1)
//            {
//                UIBarButtonItem LeftNavItems = navigationItem.RightBarButtonItems[i];
//                LeftNavList.Add(LeftNavItems);
//            }
//            else if (ItemPriority == 0)
//            {
//                UIBarButtonItem RightNavItems = navigationItem.RightBarButtonItems[i];
//                rightNavList.Add(RightNavItems);
//            }
//        }

//        navigationItem.SetLeftBarButtonItems(LeftNavList.ToArray(), false);
//        navigationItem.SetRightBarButtonItems(rightNavList.ToArray(), false);

//    }
//}


public class LeftyRightyMenuRenderer : PageRenderer
{
    public new ContentPage Element
    {
        get { return (ContentPage)base.Element; }
    }

    public override void ViewWillAppear(bool animated)
    {
        base.ViewWillAppear(animated);

        var LeftNavList = new List<UIBarButtonItem>();
        var rightNavList = new List<UIBarButtonItem>();

        var navigationItem = this.NavigationController.TopViewController.NavigationItem;

        for (int i = 0; i < Element.ToolbarItems.Count; i++)
        {
            var item = Element.ToolbarItems[Element.ToolbarItems.Count - i - 1];
            if (item.Priority == 1)
            {
                LeftNavList.Add((UIBarButtonItem)navigationItem.RightBarButtonItems[i]);
            } else
            {
                rightNavList.Add((UIBarButtonItem)navigationItem.RightBarButtonItems[i]);
            }
        }

        //for (var i = 0; i < Element.ToolbarItems.Count; i++)
        //{

        //    var reorder = (Element.ToolbarItems.Count - 1);
        //    var ItemPriority = Element.ToolbarItems[reorder - i].Priority;

        //    if (ItemPriority == 1)
        //    {
        //        UIBarButtonItem LeftNavItems = navigationItem.RightBarButtonItems[i];
        //        LeftNavList.Add(LeftNavItems);
        //    }
        //    else if (ItemPriority == 0)
        //    {
        //        UIBarButtonItem RightNavItems = navigationItem.RightBarButtonItems[i];
        //        rightNavList.Add(RightNavItems);
        //    }
        //}

        navigationItem.SetLeftBarButtonItems(LeftNavList.ToArray(), false);
        navigationItem.SetRightBarButtonItems(rightNavList.ToArray(), false);

    }
}