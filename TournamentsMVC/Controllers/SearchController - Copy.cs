using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TournamentsMVC.Controllers
{
    public class SearchController_ : Controller
    {
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly IMapperAdapter mapper;

        public SearchController_(IBookService bookService, IGenreService genreService, IMapperAdapter mapper)
        {
            if (bookService == null)
            {
                throw new ArgumentNullException("bookService");
            }

            if (genreService == null)
            {
                throw new ArgumentNullException("genreService");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            this.bookService = bookService;
            this.genreService = genreService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = new SearchViewModel();
            var genres = this.genreService.GetAllGenres();
            model.Genres = this.mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return View(model);
        }
        
        public PartialViewResult SearchBooks(SearchSubmitModel submitModel, int? page)
        {
            // TODO: increase books per page, extract constants
            int actualPage = page ?? 1;
            int booksPerPage = 3;

            var result = this.bookService.SearchBooks(submitModel.SearchWord, submitModel.ChosenGenresIds, submitModel.SortBy, actualPage, booksPerPage);
            var count = this.bookService.GetBooksCount(submitModel.SearchWord, submitModel.ChosenGenresIds);

            var resultViewModel = new SearchResultsViewModel();
            resultViewModel.BooksCount = count;
            resultViewModel.SubmitModel = submitModel;
            resultViewModel.Pages = (int)Math.Ceiling((double)count / booksPerPage);

            resultViewModel.Books = mapper.Map<IEnumerable<BookViewModel>>(result);

            return this.PartialView("_ResultsPartial", resultViewModel);
        }
    }
}