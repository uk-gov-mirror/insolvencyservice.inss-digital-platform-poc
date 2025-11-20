using INSS.Platform.Portal.Application.Resolvers;
using INSS.Platform.Portal.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages.Internal;

namespace INSS.Platform.Portal.Application.Services;

public class AddressService : BasePageModelService<AddressModel>
{
    public AddressService(
        IFormStateService formStateService, 
        IJourneyService  journeyService,
        IUserSessionResolver  userSessionResolver)
        : base(formStateService, journeyService, userSessionResolver)
    {
    }
    
    protected override void CopySourceToTargetModel(AddressModel sourceModel, AddressModel targetModel)
    {
        targetModel.AddressLine1 = sourceModel.AddressLine1;
        targetModel.AddressLine2 = sourceModel.AddressLine2;
        targetModel.TownCity = sourceModel.TownCity;
        targetModel.County = sourceModel.County;
        targetModel.Postcode = sourceModel.Postcode;
    }
}

public class SummaryListService : BasePageModelService<SummaryListModel>
{
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;
    private readonly IUserSessionResolver _userSessionResolver;

    public SummaryListService(
        IFormStateService formStateService, 
        IJourneyService  journeyService,
        IUserSessionResolver  userSessionResolver)
        : base(formStateService, journeyService, userSessionResolver)
    {
        _formStateService = formStateService;
        _journeyService = journeyService;
        _userSessionResolver = userSessionResolver;
    }
    
    protected override void CopySourceToTargetModel(SummaryListModel sourceModel, SummaryListModel targetModel)
    {
    }

    public override async Task<SummaryListModel> LoadAsync(string? pageUrl)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        var page = form.FindPage<SummaryListModel>(pageUrl!);
        _journeyService.TransitionPrevious(form, page);
        form.AddPreviousPageToSummaryList(page);
        await _formStateService.SaveAsync(_userSessionResolver.GetUserId(), form);
        return page;
    }
}