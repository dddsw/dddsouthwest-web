using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using DDDSouthWest.IdentityServer.Quickstart;
using IdentityModel;
using IdentityServer4.Test;

namespace DDDSouthWest.IdentityServer.Framework
{

  public class DummyUserStore
  {
    private readonly List<TestUser> _users;

    public DummyUserStore()
    {
      var users = new List<TestUser>
      {
        new TestUser
        {
          SubjectId = "1",
          Username = "josephwoodward",
          Password = "password",
          Claims = new List<Claim>
          {
            new Claim("name", "Joseph Woodward"),
            new Claim("role", Role.Organiser),
            new Claim("role", Role.Speaker),
            new Claim("role", Role.Registered)
          }
        }
      };
      
      /*_users = TestUsers.Users;*/
      _users = users;
    }

    public virtual bool ValidateCredentials(string username, string password)
    {
      TestUser byUsername = this.FindByUsername(username);
      if (byUsername != null)
        return byUsername.Password.Equals(password);
      return false;
    }

    /// <summary>Finds the user by subject identifier.</summary>
    /// <param name="subjectId">The subject identifier.</param>
    /// <returns></returns>
    public TestUser FindBySubjectId(string subjectId)
    {
      var user = _users.FirstOrDefault(x => x.SubjectId == subjectId);
      return user;
    }

    /// <summary>Finds the user by username.</summary>
    /// <param name="username">The username.</param>
    /// <returns></returns>
    public TestUser FindByUsername(string username)
    {
      var user = _users.FirstOrDefault(x => x.Username == username);
      return user;
    }

    /// <summary>Finds the user by external provider.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public TestUser FindByExternalProvider(string provider, string userId)
    {
      var users = _users.FirstOrDefault(x => x.ProviderName == provider);
      return users;

      /*return (TestUser) Enumerable.FirstOrDefault<TestUser>((IEnumerable<M0>) this._users, (Func<M0, bool>) (x =>
      {
        if (x.ProviderName == provider)
          return x.ProviderSubjectId == userId;
        return false;
      }));*/
    }

    /// <summary>Automatically provisions a user.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="claims">The claims.</param>
    /// <returns></returns>
/*
    public TestUser AutoProvisionUser(string provider, string userId, List<Claim> claims)
    {
      List<Claim> claimList1 = new List<Claim>();
      using (List<Claim>.Enumerator enumerator = claims.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Claim current = enumerator.Current;
          if (current.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
            claimList1.Add(new Claim("name", current.Value));
          else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(current.Type))
            claimList1.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[current.Type], current.Value));
          else
            claimList1.Add(current);
        }
      }
      List<Claim> claimList2 = claimList1;
      Func<Claim, bool> func1 = (Func<Claim, bool>) (x => x.Type == "name");
      Func<Claim, bool> func2;
      if (!Enumerable.Any<Claim>((IEnumerable<M0>) claimList2, (Func<M0, bool>) func2))
      {
        M0 m0_1 = Enumerable.FirstOrDefault<Claim>((IEnumerable<M0>) claimList1, (Func<M0, bool>) (x => x.Type == "given_name"));
        string str1 = m0_1 != null ? ((Claim) m0_1).Value : (string) null;
        M0 m0_2 = Enumerable.FirstOrDefault<Claim>((IEnumerable<M0>) claimList1, (Func<M0, bool>) (x => x.Type == "family_name"));
        string str2 = m0_2 != null ? ((Claim) m0_2).Value : (string) null;
        if (str1 != null && str2 != null)
          claimList1.Add(new Claim("name", str1 + " " + str2));
        else if (str1 != null)
          claimList1.Add(new Claim("name", str1));
        else if (str2 != null)
          claimList1.Add(new Claim("name", str2));
      }
      string uniqueId = CryptoRandom.CreateUniqueId(32);
      M0 m0 = Enumerable.FirstOrDefault<Claim>((IEnumerable<M0>) claimList1, (Func<M0, bool>) (c => c.Type == "name"));
      string str = (m0 != null ? ((Claim) m0).Value : (string) null) ?? uniqueId;
      TestUser testUser = new TestUser()
      {
        SubjectId = uniqueId,
        Username = str,
        ProviderName = provider,
        ProviderSubjectId = userId,
        Claims = (ICollection<Claim>) claimList1
      };
      this._users.Add(testUser);
      return testUser;
    }
*/
  }
}