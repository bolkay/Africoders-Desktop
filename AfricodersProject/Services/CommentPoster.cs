using AfricodersProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfricodersProject
{
    /// <summary>
    /// Responsible for consuming comment endpoints.
    /// </summary>
    public class CommentPoster
    {
        /// <summary>
        /// Reference to the posthelper class.
        /// </summary>
        PostHelper postHelper;
        //The endpoint to make a comment to.
        public bool success;
        private string CommentPostEndpoint= @"https://api.africoders.com/v1/comment?";
        /// <summary>
        /// Make a comment to the provided endpoint.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="postID"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public async Task MakeAComment(string token,int postID,string comment)
        {

            CommentPostEndpoint = CommentPostEndpoint + "pid=" + postID + "&body=" + comment;
            postHelper = new PostHelper(CommentPostEndpoint);
           
            await postHelper.MakePostAsync(token);

            success = postHelper.StatusPostSuccessful;
        }
    }
}
