using AutoMapper;
using EMS.DTO;
using EMS.Entities;

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

    }
}
