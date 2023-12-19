using FluentAssertions;
using OrderProcessing.WebApi.Exceptions;
using OrderProcessing.WebApi.Validators;

namespace OrderProcessing.Tests.Validators
{
    public class ArticleInputValidatorTests
    {
        private readonly IArticleInputValidator _articleInputValidator;

        public ArticleInputValidatorTests()
        {
            _articleInputValidator = new ArticleInputValidator();
        }

        [Fact]
        public void GivenValidaData_WhenValidate_ThenNoException()
        {
            //given
            var line = 10;
            var input = $"H,Code,Name,2.2,1.6,1.22,122.11,1,1,1,1,Group,";

            //when
            var action = () => _articleInputValidator.ValidateInputLine(input, line);

            //then
            action.Should().NotThrow();
        }

        [Fact]
        public void GivenInvalidaInputSize_WhenValidate_ThenThrows()
        {
            //given
            var line = 10;
            var input = $"H,Code,Name,2,1,1,1,1,1,1,Group,";

            //when
            var action = () => _articleInputValidator.ValidateInputLine(input, line);

            //then
            action.Should().Throw<InvalidDocumentSize>().WithMessage($"*Invalid document size in line {line}*");
        }

        [Fact]
        public void GivenInvalidaDataFormat_WhenValidate_ThenThrows()
        {
            //given
            var line = 10;
            var input = $"H,Code,Name,NotDecimal,1,1,1,1,1,1,1,Group,";

            //when
            var action = () => _articleInputValidator.ValidateInputLine(input, line);

            //then
            action.Should().Throw<InvalidTypeException>().WithMessage($"*Invalid type in line {line}*");
        }
    }
}
