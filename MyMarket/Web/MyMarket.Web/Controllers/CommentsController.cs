﻿using System.Net;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;

using MyMarket.Data.Models;
using MyMarket.Services.Data.Contracts;
using MyMarket.Web.Attributes;
using MyMarket.Web.Models.Comments;

namespace MyMarket.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            Guard.WhenArgument(commentsService, nameof(commentsService)).IsNull().Throw();

            this.commentsService = commentsService;
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult Post(string content, int adId)
        {
            var comment = new Comment()
            {
                Content = content,
                AdId = adId,
                UserId = this.User.Identity.GetUserId()
            };

            this.commentsService.Add(comment);

            var viewModel = Mapper.Map<CommentViewModel>(comment);
            viewModel.Username = this.User.Identity.Name;

            return this.PartialView("_CommentPartial", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public HttpStatusCode Update(int id, string content)
        {
            Comment comment = this.commentsService.Get(id);

            if (comment != null)
            {
                comment.Content = content;
                this.commentsService.Update(id, comment);

                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }

        [HttpPost]
        [AjaxOnly]
        public HttpStatusCode Delete(int id)
        {
            Comment comment = this.commentsService.Delete(id);

            if (comment.IsDeleted)
            {
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }
    }
}