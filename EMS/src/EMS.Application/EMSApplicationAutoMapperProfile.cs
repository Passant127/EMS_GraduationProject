using AutoMapper;
using EMS.DTO;
using EMS.Entities;
using EMS.IServices;

namespace EMS;

public class EMSApplicationAutoMapperProfile : Profile
{
    public EMSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */


        CreateMap<Recipe, RecipeDto>();
        CreateMap<CreateUpdateRecipeDto, Recipe>();
   

        // Workout Mapping
        CreateMap<Workout, WorkoutDto>();
        CreateMap<CreateUpdateWorkoutDto, Workout>();

        // Product
        CreateMap<Product, ProductDto>();
        CreateMap<CreatedUpdatedProductDto, Product>();


        //Orders




        //Cart
        CreateMap<CartItem, CartItemDto>();
        CreateMap<AddToCartDto, CartItem>();


        // contact us 

        CreateMap<ContactUs, ContactUsDto>();
        CreateMap<CreateUpdateContactUsDto, ContactUs>();


        //Fitness

        CreateMap< FitnessInfo, FitnessInfoDto>();
        CreateMap<CreateUpdateFitnessInfoDto, FitnessInfo>();



    }
}
