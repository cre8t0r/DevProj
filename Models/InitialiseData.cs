using DevelopmentProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentProject.Models
{
    public class InitialiseData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            InitialiseOccupationRating(serviceProvider);

            InitialiseRatingFactor(serviceProvider);
        }

        private static void InitialiseOccupationRating(IServiceProvider serviceProvider)
        {
            using (var context = new DevProjMainContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DevProjMainContext>>()))
            {
                if (context.OccupationRating.Any())
                {
                    return;
                }

                context.OccupationRating.AddRange(
                    new OccupationRating
                    {
                        Occupation = "Cleaner",
                        Rating = "Light Manual"
                    },

                    new OccupationRating
                    {
                        Occupation = "Doctor",
                        Rating = "Professional"
                    },

                    new OccupationRating
                    {
                        Occupation = "Author",
                        Rating = "White Collar"
                    },

                    new OccupationRating
                    {
                        Occupation = "Farmer",
                        Rating = "Heavy Manual"
                    },

                    new OccupationRating
                    {
                        Occupation = "Mechanic",
                        Rating = "Heavy Manual"
                    },

                    new OccupationRating
                    {
                        Occupation = "Florist",
                        Rating = "Light Manual"
                    }
                );
                context.SaveChanges();
            }
        }

        private static void InitialiseRatingFactor(IServiceProvider serviceProvider)
        {
            using (var context = new DevProjMainContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DevProjMainContext>>()))
            {
                if (context.RatingFactor.Any())
                {
                    return;
                }

                context.RatingFactor.AddRange(
                    new RatingFactor
                    {
                        Rating = "Professional",
                        Factor = 1.1m
                    },

                    new RatingFactor
                    {
                        Rating = "White Collar",
                        Factor = 1.45m
                    },

                    new RatingFactor
                    {
                        Rating = "Light Manual",
                        Factor = 1.70m
                    },

                    new RatingFactor
                    {
                        Rating = "Heavy Manual",
                        Factor = 2.1m
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
