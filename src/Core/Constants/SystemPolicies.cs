namespace Core.Constants;

public enum SystemPolicies
{
    IndividualRegistration,
    IndividualDetails,
    IndividualDownloadCertificate,

    GovernmentList,
    GovernmentRegistration,
    GovernmentAddSubEntity,
    GovernmentDetails,
    GovernmentAddDelegate,
    GovernmentAssignDpo,
    GovernmentChangeDelegate,
    GovernmentChangeDpo,
    GovernmentDownloadCertificate,

    PrivateSectorRegistration,
    PrivateSectorAddDelegate,
    PrivateSectorAssignDpo,
    PrivateSectorChangeDelegate,
    PrivateSectorChangeDpo,
    PrivateSectorDownloadCertificate,

    ExternalList,
    ExternalRegistration,
    ExternalCreateRequest,
    ExternalDetails,
    ExternalAddDelegate,
    ExternalAssignDpo,
    ExternalChangeDelegate,
    ExternalChangeDpo,
    ExternalDownloadCertificate,

    ExternalEntityRequestByRepresentativeList,
    ExternalEntityResponseByRepresentativeView,
    ExternalEntityResponseByRepresentativeSubmit,

    ExternalEntityRequestByAgentList,
    ExternalEntityResponseByAgentView,
    ExternalEntityResponseByAgentSubmit,


    ProductSubmit,
    ProductUpdate,
    ProductView,
    ProductList,
    ProductDelete,

    ImpactAssessmentSubmit,
    ImpactAssessmentExecute,
    ImpactAssessmentUpdate,
    ImpactAssessmentView,
    ImpactAssessmentList,

    ConsultationList,
    ConsultationView,
    ConsultationSubmit,
    ConsultationExecute,
    ConsultationUpdate,

    DataLeakNotificationList,
    DataLeakNotificationView,
    DataLeakNotificationSubmit,
    DataLeakNotificationExecute,
    DataLeakNotificationUpdate,

    ComplaintsList,
    ComplaintsView,
    ComplaintsSubmit,
    ComplaintsExecute,
    ComplaintsUpdate,
    ComplaintsReSubmit,
}