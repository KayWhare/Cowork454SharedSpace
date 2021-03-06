﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoWork454.Data;
using CoWork454.Models;
using Microsoft.Extensions.Configuration;
using CoWork454.Common;

namespace CoWork454.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly CoWork454Context _context;

        private readonly IConfiguration _Configuration;

    

        public BlogPostsController(CoWork454Context context, IConfiguration configuration)
        {
            _context = context;
        _Configuration = configuration;
    }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPost()
        {
            return await _context.BlogPost.ToListAsync();
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return blogPost;
        }

        // PUT: api/BlogPosts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(IFormFile file, int id, BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return BadRequest();
            }

            if (file != null)
            {
                string filePath = null;
                using (var stream = file.OpenReadStream())
                {
                    var connectionString = _Configuration.GetConnectionString("StorageConnection");
                    filePath = AzureStorage.AddUpdateFile(file.FileName, stream, connectionString, "Team1");
                }

                blogPost.ImageUrl = filePath;
            }

            _context.Entry(blogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogPosts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(IFormFile file, BlogPost blogPost)
        {

            if (file != null)
            {
                string filePath = null;
                using (var stream = file.OpenReadStream())
                {
                    var connectionString = _Configuration.GetConnectionString("StorageConnection");
                    filePath = AzureStorage.AddUpdateFile(file.FileName, stream, connectionString, "Team1");
                }

                blogPost.ImageUrl = filePath;
            }
            _context.BlogPost.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new { id = blogPost.Id }, blogPost);
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogPost>> DeleteBlogPost(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();

            return blogPost;
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPost.Any(e => e.Id == id);
        }
    }
}
