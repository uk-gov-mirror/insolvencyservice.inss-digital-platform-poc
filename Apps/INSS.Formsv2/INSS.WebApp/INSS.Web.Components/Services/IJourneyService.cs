using INSS.Web.Components.Models;

namespace INSS.Web.Components.Services;

public interface IJourneyService
{
    Navigation TransitionNext(FormModel form, PageModel? pageModel = null);
}

public interface IJourneyResolver
{
    PageModel? Resolve(FormModel form, BaseQuestionModel question);
}

public interface IJourneyResolver<in TQuestion> : IJourneyResolver where TQuestion : BaseQuestionModel
{
}

public sealed class DefaultJourneyResolver : IJourneyResolver //<BaseQuestionModel>
{
    public PageModel? Resolve(FormModel form, BaseQuestionModel question)
    {
        var section = form.FindSectionForQuestion(question.Id);
        return section.GetNextPageAfterQuestion(question.Id);
    }
}

// public sealed class ExampleJourneyResolver : IJourneyResolver<AddressModel>
// {
//     public PageModel? Resolve(FormModel form, BaseQuestionModel question)
//     {
//         throw new NotImplementedException();
//     }
// }