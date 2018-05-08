﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookSpace.Repositories.Contracts;
using BookSpace.Web.Models.CommentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookSpace.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IBookRepository bookRepository;
        private readonly IMapper objectMapper;

        public CommentController(ICommentRepository commentRepository, IBookRepository bookRepository, IMapper objectMapper, IApplicationUserRepository applicationUserRepository)
        {
            this.commentRepository = commentRepository;
            this.bookRepository = bookRepository;
            this.objectMapper = objectMapper;
            this.applicationUserRepository = applicationUserRepository;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCommentAsync(string commentId, string bookId, string userId)
        {
            if(commentId == null || bookId == null || userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var isAdmin = this.User.IsInRole("Admin");

            var currentLoggedUser = await this.applicationUserRepository.GetAsync(u => u.Id == userId);

            var isOwner = currentLoggedUser.Id == userId;

            if(!isAdmin && !isOwner)
            {
                return RedirectToAction("Index", "Home");
            }

            var commentToDelete = await this.commentRepository.GetAsync(c => c.CommentId == commentId);
            await this.commentRepository.DeleteAsync(commentToDelete);

            var comments = await this.bookRepository.GetBookCommentsAsync(bookId);
            var commentsViewModel = this.objectMapper.Map<IEnumerable<CommentViewModel>>(comments);

            foreach (var comment in commentsViewModel)
            {
                var commentCreatorId = comment.UserId;
                var currentUser = await this.applicationUserRepository.GetUserByUsernameAsync(this.User.Identity.Name);
                var isCreator = commentCreatorId == currentUser.Id;

                comment.CanEdit = isAdmin || isCreator;

                var user = await this.applicationUserRepository.GetByIdAsync(comment.UserId);
                comment.Author = user.UserName;
            }

            return PartialView("Book/_BookCommentsPartial", new KeyValuePair<string, IEnumerable<CommentViewModel>>(bookId, commentsViewModel));
        }
    }
}