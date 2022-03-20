using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : Controller
    {
        private IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        
        [HttpGet("getfavoritedetailsbyuserid")]
        public IActionResult GetFavoriteDetailsByUserId(int userId)
        {
            var result = _favoriteService.GetFavoriteDetailsByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getfavoritesbyuserid")]
        public IActionResult GetFavoritesByUserId(int userId)
        {
            var result = _favoriteService.GetFavoritesByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addfavorite")]
        public IActionResult AddFavorite(Favorite favorite)
        {
            var result = _favoriteService.Add(favorite);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteFavorite")]
        public IActionResult DeleteFavorite(Favorite favorite)
        {
            var result = _favoriteService.Delete(favorite);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}