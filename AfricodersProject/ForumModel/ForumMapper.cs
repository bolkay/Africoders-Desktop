using AfricodersProject.ForumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfricodersProject
{
    public class ForumMapper
    {
        public List<MultiCategory> GeneralCategories(ForumFeed forumFeed)
        {
            List<MultiCategory> generalCategories = new List<MultiCategory>();
            //Add the chat category
            generalCategories.Add(new MultiCategory
            {
                category = forumFeed.General._0.category,
                container = forumFeed.General._0.container,
                board = forumFeed.General._0.board,
                path = forumFeed.General._0.path,
                fid = forumFeed.General._0.fid,
                pid = forumFeed.General._0.pid,
                tid = forumFeed.General._0.tid,
                views = forumFeed.General._0.views,
                threads = forumFeed.General._0.threads,
                comments = forumFeed.General._0.comments,
                last_post = forumFeed.General._0.last_post,
                last_reply = forumFeed.General._0.last_reply,
                latest = forumFeed.General._0.latest
            });
            //Add the Africoders categroy
            generalCategories.Add(new MultiCategory
            {
                category = forumFeed.General._16.category,
                container = forumFeed.General._16.container,
                board = forumFeed.General._16.board,
                path = forumFeed.General._16.path,
                fid = forumFeed.General._16.fid,
                pid = forumFeed.General._16.pid,
                tid = forumFeed.General._16.tid,
                views = forumFeed.General._16.views,
                threads = forumFeed.General._16.threads,
                comments = forumFeed.General._16.comments,
                last_post = forumFeed.General._16.last_post,
                last_reply = forumFeed.General._16.last_reply,
                latest = forumFeed.General._16.latest
            });
            //Add the ideas category
            generalCategories.Add(new MultiCategory
            {
                category = forumFeed.General._17.category,
                container = forumFeed.General._17.container,
                board = forumFeed.General._17.board,
                path = forumFeed.General._17.path,
                fid = forumFeed.General._17.fid,
                pid = forumFeed.General._17.pid,
                tid = forumFeed.General._17.tid,
                views = forumFeed.General._17.views,
                threads = forumFeed.General._17.threads,
                comments = forumFeed.General._17.comments,
                last_post = forumFeed.General._17.last_post,
                last_reply = forumFeed.General._17.last_reply,
                latest = forumFeed.General._17.latest
            });

            return generalCategories;
        }

        public List<MultiCategory> WebCategories(ForumFeed forumFeed) {
            List<MultiCategory> webCategories = new List<MultiCategory>();
            webCategories.Add(new MultiCategory
            {
                category = forumFeed.WebDesign._1.category,
                container = forumFeed.WebDesign._1.container,
                board = forumFeed.WebDesign._1.board,
                path = forumFeed.WebDesign._1.path,
                fid = forumFeed.WebDesign._1.fid,
                pid = forumFeed.WebDesign._1.pid,
                tid = forumFeed.WebDesign._1.tid,
                views = forumFeed.WebDesign._1.views,
                threads = forumFeed.WebDesign._1.threads,
                comments = forumFeed.WebDesign._1.comments,
                last_post = forumFeed.WebDesign._1.last_post,
                last_reply = forumFeed.WebDesign._1.last_reply,
                latest = forumFeed.WebDesign._1.latest
            });
            webCategories.Add(new MultiCategory
            {
                category = forumFeed.WebDesign._2.category,
                container = forumFeed.WebDesign._2.container,
                board = forumFeed.WebDesign._2.board,
                path = forumFeed.WebDesign._2.path,
                fid = forumFeed.WebDesign._2.fid,
                pid = forumFeed.WebDesign._2.pid,
                tid = forumFeed.WebDesign._2.tid,
                views = forumFeed.WebDesign._2.views,
                threads = forumFeed.WebDesign._2.threads,
                comments = forumFeed.WebDesign._2.comments,
                last_post = forumFeed.WebDesign._2.last_post,
                last_reply = forumFeed.WebDesign._2.last_reply,
                latest = forumFeed.WebDesign._2.latest
            });
            webCategories.Add(new MultiCategory
            {
                category = forumFeed.WebDesign._3.category,
                container = forumFeed.WebDesign._3.container,
                board = forumFeed.WebDesign._3.board,
                path = forumFeed.WebDesign._3.path,
                fid = forumFeed.WebDesign._3.fid,
                pid = forumFeed.WebDesign._3.pid,
                tid = forumFeed.WebDesign._3.tid,
                views = forumFeed.WebDesign._3.views,
                threads = forumFeed.WebDesign._3.threads,
                comments = forumFeed.WebDesign._3.comments,
                last_post = forumFeed.WebDesign._3.last_post,
                last_reply = forumFeed.WebDesign._3.last_reply,
                latest = forumFeed.WebDesign._3.latest
            });
            webCategories.Add(new MultiCategory
            {
                category = forumFeed.WebDesign._4.category,
                container = forumFeed.WebDesign._4.container,
                board = forumFeed.WebDesign._4.board,
                path = forumFeed.WebDesign._4.path,
                fid = forumFeed.WebDesign._4.fid,
                pid = forumFeed.WebDesign._4.pid,
                tid = forumFeed.WebDesign._4.tid,
                views = forumFeed.WebDesign._4.views,
                threads = forumFeed.WebDesign._4.threads,
                comments = forumFeed.WebDesign._4.comments,
                last_post = forumFeed.WebDesign._4.last_post,
                last_reply = forumFeed.WebDesign._4.last_reply,
                latest = forumFeed.WebDesign._4.latest
            });
            return webCategories;
        }

        public List<MultiCategory> MobileCategories(ForumFeed forumFeed)
        {
            List<MultiCategory> multiCategories = new List<MultiCategory>();
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Mobile._5.category,
                container = forumFeed.Mobile._5.container,
                board = forumFeed.Mobile._5.board,
                path = forumFeed.Mobile._5.path,
                fid = forumFeed.Mobile._5.fid,
                pid = forumFeed.Mobile._5.pid,
                tid = forumFeed.Mobile._5.tid,
                views = forumFeed.Mobile._5.views,
                threads = forumFeed.Mobile._5.threads,
                comments = forumFeed.Mobile._5.comments,
                last_post = forumFeed.Mobile._5.last_post,
                last_reply = forumFeed.Mobile._5.last_reply,
                latest = forumFeed.Mobile._5.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Mobile._6.category,
                container = forumFeed.Mobile._6.container,
                board = forumFeed.Mobile._6.board,
                path = forumFeed.Mobile._6.path,
                fid = forumFeed.Mobile._6.fid,
                pid = forumFeed.Mobile._6.pid,
                tid = forumFeed.Mobile._6.tid,
                views = forumFeed.Mobile._6.views,
                threads = forumFeed.Mobile._6.threads,
                comments = forumFeed.Mobile._6.comments,
                last_post = forumFeed.Mobile._6.last_post,
                last_reply = forumFeed.Mobile._6.last_reply,
                latest = forumFeed.Mobile._6.latest
            });
            return multiCategories;
        }

        public List<MultiCategory> AppCategories(ForumFeed forumFeed)
        {
            List<MultiCategory> multiCategories = new List<MultiCategory>();
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.AppDev._10.category,
                container = forumFeed.AppDev._10.container,
                board = forumFeed.AppDev._10.board,
                path = forumFeed.AppDev._10.path,
                fid = forumFeed.AppDev._10.fid,
                pid = forumFeed.AppDev._10.pid,
                tid = forumFeed.AppDev._10.tid,
                views = forumFeed.AppDev._10.views,
                threads = forumFeed.AppDev._10.threads,
                comments = forumFeed.AppDev._10.comments,
                last_post = forumFeed.AppDev._10.last_post,
                last_reply = forumFeed.AppDev._10.last_reply,
                latest = forumFeed.AppDev._10.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.AppDev._11.category,
                container = forumFeed.AppDev._11.container,
                board = forumFeed.AppDev._11.board,
                path = forumFeed.AppDev._11.path,
                fid = forumFeed.AppDev._11.fid,
                pid = forumFeed.AppDev._11.pid,
                tid = forumFeed.AppDev._11.tid,
                views = forumFeed.AppDev._11.views,
                threads = forumFeed.AppDev._11.threads,
                comments = forumFeed.AppDev._11.comments,
                last_post = forumFeed.AppDev._11.last_post,
                last_reply = forumFeed.AppDev._11.last_reply,
                latest = forumFeed.AppDev._11.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.AppDev._7.category,
                container = forumFeed.AppDev._7.container,
                board = forumFeed.AppDev._7.board,
                path = forumFeed.AppDev._7.path,
                fid = forumFeed.AppDev._7.fid,
                pid = forumFeed.AppDev._7.pid,
                tid = forumFeed.AppDev._7.tid,
                views = forumFeed.AppDev._7.views,
                threads = forumFeed.AppDev._7.threads,
                comments = forumFeed.AppDev._7.comments,
                last_post = forumFeed.AppDev._7.last_post,
                last_reply = forumFeed.AppDev._7.last_reply,
                latest = forumFeed.AppDev._7.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.AppDev._8.category,
                container = forumFeed.AppDev._8.container,
                board = forumFeed.AppDev._8.board,
                path = forumFeed.AppDev._8.path,
                fid = forumFeed.AppDev._8.fid,
                pid = forumFeed.AppDev._8.pid,
                tid = forumFeed.AppDev._8.tid,
                views = forumFeed.AppDev._8.views,
                threads = forumFeed.AppDev._8.threads,
                comments = forumFeed.AppDev._8.comments,
                last_post = forumFeed.AppDev._8.last_post,
                last_reply = forumFeed.AppDev._8.last_reply,
                latest = forumFeed.AppDev._8.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.AppDev._9.category,
                container = forumFeed.AppDev._9.container,
                board = forumFeed.AppDev._9.board,
                path = forumFeed.AppDev._9.path,
                fid = forumFeed.AppDev._9.fid,
                pid = forumFeed.AppDev._9.pid,
                tid = forumFeed.AppDev._9.tid,
                views = forumFeed.AppDev._9.views,
                threads = forumFeed.AppDev._9.threads,
                comments = forumFeed.AppDev._9.comments,
                last_post = forumFeed.AppDev._9.last_post,
                last_reply = forumFeed.AppDev._9.last_reply,
                latest = forumFeed.AppDev._9.latest
            });
            return multiCategories;
        }

        public List<MultiCategory> DatabaseCategories(ForumFeed forumFeed)
        {
            List<MultiCategory> multiCategories = new List<MultiCategory>();
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Database._12.category,
                container = forumFeed.Database._12.container,
                board = forumFeed.Database._12.board,
                path = forumFeed.Database._12.path,
                fid = forumFeed.Database._12.fid,
                pid = forumFeed.Database._12.pid,
                tid = forumFeed.Database._12.tid,
                views = forumFeed.Database._12.views,
                threads = forumFeed.Database._12.threads,
                comments = forumFeed.Database._12.comments,
                last_post = forumFeed.Database._12.last_post,
                last_reply = forumFeed.Database._12.last_reply,
                latest = forumFeed.Database._12.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Database._13.category,
                container = forumFeed.Database._13.container,
                board = forumFeed.Database._13.board,
                path = forumFeed.Database._13.path,
                fid = forumFeed.Database._13.fid,
                pid = forumFeed.Database._13.pid,
                tid = forumFeed.Database._13.tid,
                views = forumFeed.Database._13.views,
                threads = forumFeed.Database._13.threads,
                comments = forumFeed.Database._13.comments,
                last_post = forumFeed.Database._13.last_post,
                last_reply = forumFeed.Database._13.last_reply,
                latest = forumFeed.Database._13.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Database._14.category,
                container = forumFeed.Database._14.container,
                board = forumFeed.Database._14.board,
                path = forumFeed.Database._14.path,
                fid = forumFeed.Database._14.fid,
                pid = forumFeed.Database._14.pid,
                tid = forumFeed.Database._14.tid,
                views = forumFeed.Database._14.views,
                threads = forumFeed.Database._14.threads,
                comments = forumFeed.Database._14.comments,
                last_post = forumFeed.Database._14.last_post,
                last_reply = forumFeed.Database._14.last_reply,
                latest = forumFeed.Database._14.latest
            });
            return multiCategories;
        }


        public List<MultiCategory> CommunityCategories(ForumFeed forumFeed)
        {
            List<MultiCategory> multiCategories = new List<MultiCategory>();
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Community._15.category,
                container = forumFeed.Community._15.container,
                board = forumFeed.Community._15.board,
                path = forumFeed.Community._15.path,
                fid = forumFeed.Community._15.fid,
                pid = forumFeed.Community._15.pid,
                tid = forumFeed.Community._15.tid,
                views = forumFeed.Community._15.views,
                threads = forumFeed.Community._15.threads,
                comments = forumFeed.Community._15.comments,
                last_post = forumFeed.Community._15.last_post,
                last_reply = forumFeed.Community._15.last_reply,
                latest = forumFeed.Community._15.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Community._18.category,
                container = forumFeed.Community._18.container,
                board = forumFeed.Community._18.board,
                path = forumFeed.Community._18.path,
                fid = forumFeed.Community._18.fid,
                pid = forumFeed.Community._18.pid,
                tid = forumFeed.Community._18.tid,
                views = forumFeed.Community._18.views,
                threads = forumFeed.Community._18.threads,
                comments = forumFeed.Community._18.comments,
                last_post = forumFeed.Community._18.last_post,
                last_reply = forumFeed.Community._18.last_reply,
                latest = forumFeed.Community._18.latest
            });
            multiCategories.Add(new MultiCategory
            {
                category = forumFeed.Community._19.category,
                container = forumFeed.Community._19.container,
                board = forumFeed.Community._19.board,
                path = forumFeed.Community._19.path,
                fid = forumFeed.Community._19.fid,
                pid = forumFeed.Community._19.pid,
                tid = forumFeed.Community._19.tid,
                views = forumFeed.Community._19.views,
                threads = forumFeed.Community._19.threads,
                comments = forumFeed.Community._19.comments,
                last_post = forumFeed.Community._19.last_post,
                last_reply = forumFeed.Community._19.last_reply,
                latest = forumFeed.Community._19.latest
            });
            return multiCategories;
        }

    }

    /// <summary>
    /// Each item in the original categories will be mapped.
    /// </summary>
    public class MultiCategory
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }
}
