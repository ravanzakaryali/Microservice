﻿using PostService.Domain;
using PostService.Inerfaces;
using PostService.Inerfaces.Interfaces;
using PostService.Persistence.DataContext;
using PostService.Repository.Implementations;

namespace PostService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IPostRepository _postRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
