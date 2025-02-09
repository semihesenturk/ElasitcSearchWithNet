using ES.ElasticSearch.API.Models;

namespace ES.ElasticSearch.API.Dtos;

public record ProductFeatureDto(int Width, int Height, EColor Color)
{}