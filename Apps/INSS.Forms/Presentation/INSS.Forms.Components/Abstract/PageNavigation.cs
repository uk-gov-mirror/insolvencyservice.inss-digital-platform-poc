//using Microsoft.AspNetCore.Components;

//namespace INSS.Forms.Components.Abstract
//{
//    public class PageNavigation : ComponentBase
//    {
//        private NavigationItem navigationItem = new(0, []);

//        //public bool IsVisible(string pageName)
//        //{
//        //    if (navigationItem.PageNames.Length > 0)
//        //    {
//        //        if(navigationItem.PageNames.Any(p => p == pageName))
//        //        {
//        //            return true;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        return false;
//        //    }
//        //}

//        public void Next()
//        {
//            if (navigationItem.CurrentPageIndex < navigationItem.PageNames.Length - 1)
//            {
//                navigationItem.CurrentPageIndex++;
//            }
//        }

//        public void Back()
//        {
//            if (navigationItem.CurrentPageIndex > 0)
//            {
//                navigationItem.CurrentPageIndex--;
//            }
//        }   

//        struct NavigationItem
//        {
//            public int CurrentPageIndex { get; set; }
//            public string[] PageNames { get; set; }
//            public NavigationItem(int currentPageIndex, string[] pageNames)
//            {
//                CurrentPageIndex = currentPageIndex;
//                PageNames = pageNames;
//            }
//        }
//    }
//}
