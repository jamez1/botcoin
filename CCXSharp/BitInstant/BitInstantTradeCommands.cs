using System;
using CCXSharp.BaseClasses;
using CCXSharp.BitInstant.Models;
using RestSharp;

namespace CCXSharp.BitInstant
{
    public interface IBitInstantTradeCommands
    {
        Fee CalculateFee(string paymethod, string destExchange, string amount, string currency);
        Quote GetQuote(string paymethod, decimal amount, string destExchange, string dob, string destAccount, string firstName, string lastName, string notifyEmail);
    }

    public class BitInstantTradeCommands : IBitInstantTradeCommands
    {
        protected IBitInstantRestClient restClient = new BitInstantRestClient();

        public Fee CalculateFee(string paymethod, string destExchange, string amount, string currency)
        {
            return restClient.GetResponse<Fee>(String.Format("/CalculateFee/{0}/{1}/{2}/{3}", paymethod, destExchange, amount, currency), Method.GET, null, AccessType.Public);
        }

        public Quote GetQuote(string paymethod, decimal amount, string destExchange, string dob, string destAccount, string firstName, string lastName, string notifyEmail)
        {
            QuoteRequest request = new QuoteRequest
            {
                PayMethod = paymethod,
                Amount = amount,
                DestExchange = destExchange,
                DoB = dob,
                DestAccount = destAccount,
                FirstName = firstName,
                LastName = lastName,
                NotifyEmail = notifyEmail
            };
           return restClient.GetResponse<Quote>("/GetQuote", Method.POST, request, AccessType.Public);
        }
    }
}