using System;
using System.Collections.Generic;

namespace PropertyService.Entities
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string county { get; set; }
        public object district { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public object zipPlus4 { get; set; }
    }

    public class Financial
    {
        public object capRate { get; set; }
        public string occupancy { get; set; }
        public bool isSection8 { get; set; }
        public DateTime leaseStartDate { get; set; }
        public DateTime leaseEndDate { get; set; }
        public double listPrice { get; set; }
        public object salePrice { get; set; }
        public object marketValue { get; set; }
        public object monthlyHoa { get; set; }
        public object monthlyManagementFees { get; set; }
        public double monthlyRent { get; set; }
        public object netYield { get; set; }
        public object turnOverFee { get; set; }
        public object rehabCosts { get; set; }
        public object rehabDate { get; set; }
        public double yearlyInsuranceCost { get; set; }
        public double yearlyPropertyTaxes { get; set; }
        public bool isCashOnly { get; set; }
    }

    public class Physical
    {
        public double bathRooms { get; set; }
        public double bedRooms { get; set; }
        public string parcelNumber { get; set; }
        public bool isPool { get; set; }
        public object lotSize { get; set; }
        public int squareFeet { get; set; }
        public object stories { get; set; }
        public object units { get; set; }
        public int yearBuilt { get; set; }
        public object zipYearBuilt { get; set; }
    }

    public class Score
    {
        public object conditionScore { get; set; }
        public string crimeScore { get; set; }
        public int neighborhoodScore { get; set; }
        public object overallScore { get; set; }
        public object qualityScore { get; set; }
        public string schoolScore { get; set; }
        public string charterSchoolScore { get; set; }
        public string floodRiskScore { get; set; }
        public object walkabilityScore { get; set; }
    }

    public class Valuation
    {
        public object avmBpoValue { get; set; }
        public object avmBpoAdjValue { get; set; }
        public object avmBpoDate { get; set; }
        public double rsAvmValue { get; set; }
        public object rsAvmDate { get; set; }
        public object rsBpoMergeValue { get; set; }
    }

    public class Photo
    {
        public int id { get; set; }
        public string guid { get; set; }
        public string resourceType { get; set; }
        public bool isPublic { get; set; }
        public object description { get; set; }
        public string filename { get; set; }
        public object sizeInByte { get; set; }
        public string contentType { get; set; }
        public string url { get; set; }
        public string urlMedium { get; set; }
        public string urlSmall { get; set; }
        public object textContent { get; set; }
    }

    public class FloorPlan
    {
        public int id { get; set; }
        public string guid { get; set; }
        public string resourceType { get; set; }
        public bool isPublic { get; set; }
        public object description { get; set; }
        public string filename { get; set; }
        public object sizeInByte { get; set; }
        public string contentType { get; set; }
        public string url { get; set; }
        public string urlMedium { get; set; }
        public string urlSmall { get; set; }
        public object textContent { get; set; }
    }

    public class Resources
    {
        public List<Photo> photos { get; set; }
        public List<FloorPlan> floorPlans { get; set; }
        public List<object> threeDRenderings { get; set; }
        public List<object> audios { get; set; }
    }

    public class LeaseSummary
    {
        public string occupancy { get; set; }
        public object leasingStatus { get; set; }
        public object marketedRent { get; set; }
        public object paymentStatus { get; set; }
        public DateTime leaseStartDate { get; set; }
        public DateTime leaseEndDate { get; set; }
        public double monthlyRent { get; set; }
        public object securityDepositAmount { get; set; }
        public object hasPet { get; set; }
        public object petFeeAmount { get; set; }
        public bool isPetsDeposit { get; set; }
        public object petsDepositAmount { get; set; }
        public bool isLeaseConcessions { get; set; }
        public bool isRentersInsuranceRequired { get; set; }
        public bool isSection8 { get; set; }
        public bool isTenantBackgroundChecked { get; set; }
        public bool isTenantIncomeAbove3x { get; set; }
        public object isTenantMayTerminateEarly { get; set; }
        public object isTenantPurchaseOption { get; set; }
    }

    public class UtilitiesOwnership
    {
        public string electric { get; set; }
        public string gas { get; set; }
        public string water { get; set; }
        public string garbage { get; set; }
        public string pool { get; set; }
        public string landscaping { get; set; }
        public string pestControl { get; set; }
    }

    public class ApplianceOwnership
    {
        public string refrigerator { get; set; }
        public string dishwasher { get; set; }
        public string washer { get; set; }
        public string dryer { get; set; }
        public string microwave { get; set; }
        public string stove { get; set; }
    }

    public class Lease
    {
        public LeaseSummary leaseSummary { get; set; }
        public UtilitiesOwnership utilitiesOwnership { get; set; }
        public ApplianceOwnership applianceOwnership { get; set; }
    }

    public class GoogleMapOption
    {
        public bool hasStreetView { get; set; }
        public int povHeading { get; set; }
        public int povPitch { get; set; }
        public double povLatitude { get; set; }
        public double povLongitude { get; set; }
    }

    public class Property
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public object buyerAccountId { get; set; }
        public object buyerUserId { get; set; }
        public object externalId { get; set; }
        public object programId { get; set; }
        public bool isDwellCertified { get; set; }
        public bool isAllowOffer { get; set; }
        public bool isAllowPreview { get; set; }
        public bool isFeatured { get; set; }
        public bool isRentGuaranteed { get; set; }
        public bool allowRentGuaranteedOptout { get; set; }
        public bool isSecuritized { get; set; }
        public bool isHot { get; set; }
        public bool isNew { get; set; }
        public bool isBargain { get; set; }
        public bool isDiligenceVaultUnlocked { get; set; }
        public bool isPropertyManagerOfferRetain { get; set; }
        public bool isHoa { get; set; }
        public bool hasAudio { get; set; }
        public bool hasDiligenceVaultDocuments { get; set; }
        public int market { get; set; }
        public int daysOnMarket { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public object propertyName { get; set; }
        public string description { get; set; }
        public object highlights { get; set; }
        public string mainImageUrl { get; set; }
        public object personalProperties { get; set; }
        public object diligenceVaultSummary { get; set; }
        public object featuredReason { get; set; }
        public string status { get; set; }
        public string allowedFundingTypes { get; set; }
        public string allowableSaleTypes { get; set; }
        public string visibility { get; set; }
        public string priceVisibility { get; set; }
        public string propertyType { get; set; }
        public object certificationLevel { get; set; }
        public object escrowClosingDate { get; set; }
        public Address address { get; set; }
        public Financial financial { get; set; }
        public Physical physical { get; set; }
        public Score score { get; set; }
        public Valuation valuation { get; set; }
        public Resources resources { get; set; }
        public object manager { get; set; }
        public object seller { get; set; }
        public object sellerBroker { get; set; }
        public object hoa { get; set; }
        public Lease lease { get; set; }
        public List<object> diligences { get; set; }
        public GoogleMapOption googleMapOption { get; set; }
        public object inspectionType { get; set; }
    }


}
