using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cafe.Data.Models;
using Cafe.API.IRepository;
#nullable disable

namespace Cafe.API.Repository
{
    public class MilkShakeRepository : IMilkShakeRepository
    {
        private readonly EspressoEcstasyContext _context;

        public MilkShakeRepository(EspressoEcstasyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MilkShake>> GetMilkShakesAsync()
        {
            return await _context.MilkShakes.ToListAsync();
        }

        public async Task<MilkShake> GetMilkShakeAsync(int id)
        {
            return await _context.MilkShakes.FindAsync(id);
        }

        public async Task UpdateMilkShakeAsync(int id, MilkShake milkShake)
        {
            var existingCart = await _context.MilkShakes.FirstOrDefaultAsync(c => c.Mid == id) ?? throw new ArgumentException("Cart not found");
            try
            {
                existingCart.Offer = milkShake.Offer;
                existingCart.OfferPrice = milkShake.OfferPrice;
                existingCart.OriginalPrice = milkShake.OriginalPrice;
                existingCart.Stock = milkShake.Stock;
                existingCart.Mname = milkShake.Mname;
                existingCart.Image = milkShake.Image;
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public async Task CreateMilkShakeAsync(MilkShake milkShake)
        {
            _context.MilkShakes.Add(milkShake);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMilkShakeAsync(int id)
        {
            var milkShake = await _context.MilkShakes.FindAsync(id) ?? throw new ArgumentException("MilkShake not found.");
            _context.MilkShakes.Remove(milkShake);
            await _context.SaveChangesAsync();
        }
    }
}