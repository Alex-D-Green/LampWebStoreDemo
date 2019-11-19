using FluentValidation;


namespace LampStore.Infrastructure.WebApi.ApiModels
{
    public sealed class PairOfLampsApi
    {
        public int FstLampId { get; set; }

        public int SndLampId { get; set; }
    }


    public class PairOfLampsApiValidator: AbstractValidator<PairOfLampsApi>
    {
        public PairOfLampsApiValidator()
        {
            RuleFor(o => o.FstLampId).GreaterThan(0);
            RuleFor(o => o.SndLampId).GreaterThan(0);
            RuleFor(o => o.FstLampId).NotEqual(o => o.SndLampId)
                .WithMessage($"'{nameof(PairOfLampsApi.FstLampId)}' must not be equal to '{nameof(PairOfLampsApi.SndLampId)}'.");
        }
    }
}
