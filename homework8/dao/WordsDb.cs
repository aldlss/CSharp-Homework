using System;
using System.Linq;
using homework8.model;
using Microsoft.EntityFrameworkCore;

namespace homework8.dao
{
    internal class WordsDb: DbContext
    {
        public DbSet<Word> Words { get; set; }
        private readonly string _dbPath;
        private int _wordNum;
        private readonly Random _random;

        private WordsDb(string dbPath)
        {
            _dbPath = dbPath;
            _random = new Random((int)DateTimeOffset.Now.ToUnixTimeMilliseconds());
        }

        public static WordsDb? CreateWordsDbConnect(string dbPath)
        {
            var db = new WordsDb(dbPath);
            if (db.Database.EnsureCreated())
                return null;
            db._wordNum = db.Words.Count();
            return db;
        }

        public Word GetARandomWord()
        {
            var index = _random.Next(0, _wordNum);
            return Words.Skip(index).First();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }
    }
}
