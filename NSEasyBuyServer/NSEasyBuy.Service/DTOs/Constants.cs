using System;
using System.Collections.Generic;
using System.Text;

namespace NSEasyBuy.Service.DTOs
{
    public struct ResponseMessage
        {
            //Server Error
            public const string SERVER_ERROR = "Server error!!!";
            //Authentication
            public const string USER_SIGINUP_SUCCESSFULLY = "User signup successfully";
            public const string USER_LOGIN_SUCCESSFULLY = "User login successfully";
            public const string USER_LOGIN_INCORRECT_EMAIL_OR_PASSWORD = "Sorry, the email and password you entered do not match. Please try again.";
            public const string USER_SIGNUP_BAD_REQUEST = "Required field(s) are missing or password does not contain combination of capital leter, small letter, numbers and special charecters.";
            public const string USER_SIGNUP_FAILED = "User signup failed";
            public const string USER_ALREADY_EXIST = "Your email is already registered with {0}.";
            public const string USER_LOGIN_FAILED = "User login failed";
            public const string USER_EMAIL_DOES_NOT_EXIST = "Email does not exist.";
            public const string USER_INVALID_EMAIL = "The email you entered is not valid. Please provide a valid email address.";
            public const string USER_PASSWORD_CANNOT_BE_CHANGED = "The password cannot be changed.";
            public const string USER_PASSWORD_CHANGED_SUCCESSFULLY = "Your password is changed successfully.";
            public const string USER_DOES_NOT_EXIST = "User does not exist.";
            public const string USER_INVALID_RESET_REQUEST = "The userId/token you entered is not valid. Please provide a valid userId and token.";
            public const string USER_REGISTERED_WITH_SOCIAL_SITE = "You have registered with {0} and its not possible to {1} password from BookNordics.";
            //
            public const string DATA_RETURN_SUCCESSFULLY = "Data returned successfully.";
            public const string MASTER_DATA_CANNOT_BE_RETURN = "Server error!!!";
            //Product
            public const string PRODUCT_LIST_RETURN_SUCCESSFULLY = "Product list returned successfully.";
            public const string PRODUCT_LIST_EMPTY = "No product found.";
            public const string PRODUCT_LIST_CANNOT_BE_RETURN = "Server error!!!";
            public const string PRODUCT_SAVED_SUCCESSFULLY = "Product saved successfully.";
            public const string PRODUCT_CAN_NOT_BE_SAVED = "Product can not be saved.";
            public const string PRODUCT_DELETED_SUCCESSFULLY = "Product deleted succcessfully.";
            public const string PRODUCT_NOT_FOUND = "Product is not found.";
            //
            public const string BAD_REQUEST = "Bad request!!!";
            //Partner
            public const string PARTNER_LIST_RETURN_SUCCESSFULLY = "Partner list returned successfully.";
            public const string PARTNER_RETURN_SUCCESSFULLY = "Partner returned successfully.";
            public const string PARTNER_LIST_CANNOT_BE_RETURN = "Server error!!!";
            public const string PARTNER_SAVED_SUCCESSFULLY = "Partner saved successfully.";
            public const string PARTNER_CAN_NOT_BE_SAVED = "Partner can not be saved.";
            public const string PARTNER_DELETED_SUCCESSFULLY = "Partner deleted succcessfully.";
            public const string PARTNER_NOT_FOUND = "Partner is not found.";

            //Payment
            public const string PAYMENT_INVALID_PAYMENT_ORDER_REQUEST = "Invalid Payment ORDER Request Sent!!!";
            public const string PAYMENT_INVALID_PAYMENT_QUERY_REQUEST = "Invalid Payment QUERY Request Sent!!!";
            public const string PAYMENT_PROCESS_SUCCESS_MSG = "Payment Order Successful!!!";
            public const string PAYMENT_QUERY_SUCCESS_MSG = "Payment Query Successful!!!";
            public const string PAYMENT_NETS_PROCESSING_ERROR_MSG = "There was an error in processing the request. Please review the error description!!!";

        }
    
}
