﻿// Project Renfrew
// Copyright(C) 2017 Stephen Workman (workman.stephen@gmail.com)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
//

using Moq;

using NUnit.Framework;

using Renfrew.Grammar;
using Renfrew.Grammar.Exceptions;
using Renfrew.NatSpeakInterop;

namespace GrammarTests {

   [TestFixture]
   public class NestedRuleTests {

      private class TestGrammar : Grammar {
         public TestGrammar()
            :base (new Mock<IGrammarService>().Object) {
         }
         public override void Dispose() { }
         public override void Initialize() { }
      }

      private TestGrammar _grammar;

      [SetUp]
      public void SetUp() {
         _grammar = new TestGrammar();
      }

      [Test]
      public void InvokingSimpleNestedRuleWithValidWordSequenceShouldSucceed() {
         _grammar.AddRule("outer", e => e
            .Say("Something").WithRule("inner")
         );
         _grammar.AddRule("inner", e => e
            .Say("Good")
         );

         _grammar.InvokeRule(1, new [] { "Something", "Good" });
      }

      [Test]
      public void InvokingSimpleNestedRuleWithInvalidWordSequenceShouldFail() {
         Assert.That(() => {
            _grammar.AddRule("outer", e => e
               .Say("Something").WithRule("inner")
            );
            _grammar.AddRule("inner", e => e
               .Say("Good")
            );

            _grammar.InvokeRule(1, new[] { "Something", "Strange" });
         }, Throws.InstanceOf<InvalidSequenceInCallbackException>());
      }

      [Test]
      public void InvokingSimpleNestedRuleWithTooManyWordsShouldFail() {
         Assert.That(() => {
            _grammar.AddRule("outer", e => e
               .Say("Something").WithRule("inner")
            );
            _grammar.AddRule("inner", e => e
               .Say("Good")
            );

            _grammar.InvokeRule(1, new[] { "Something", "Good", "To", "Eat" });
         }, Throws.InstanceOf<TooManyWordsInCallbackException>());
      }

      [Test]
      public void InvokingSimpleNestedRuleWithTooFewWordsShouldFail() {
         Assert.That(() => {
            _grammar.AddRule("outer", e => e
               .Say("Something").WithRule("inner")
            );
            _grammar.AddRule("inner", e => e
               .Say("Good")
            );

            _grammar.InvokeRule(1, new[] { "Something" });
         }, Throws.InstanceOf<InvalidSequenceInCallbackException>());
      }

      [Test]
      public void InvokingSimpleNestedRuleWithContinuationWithValidWordSequenceShouldSucceed() {
         _grammar.AddRule("outer", e => e
            .Say("Something").WithRule("inner").Say("To").Say("Eat")
         );
         _grammar.AddRule("inner", e => e
            .Say("Good")
         );

         _grammar.InvokeRule(1, new[] { "Something", "Good", "To", "Eat" });
      }

      [Test]
      public void InvokingSimpleRuleWithOptionalNestedRuleShouldSucceedWhenOptionalRuleElementsAreIncluded() {
         _grammar.AddRule("outer", e => e
            .Say("Something").OptionallyWithRule("inner").Say("To").Say("Eat")
         );
         _grammar.AddRule("inner", e => e
            .Say("Good")
         );

         _grammar.InvokeRule(1, new[] { "Something", "Good", "To", "Eat" });
      }

      [Test]
      public void InvokingSimpleRuleWithOptionalNestedRuleShouldSucceedWhenOptionalRuleElementsAreOmitted() {
         _grammar.AddRule("outer", e => e
            .Say("Something").OptionallyWithRule("inner").Say("To").Say("Eat")
         );
         _grammar.AddRule("inner", e => e
            .Say("Good")
         );

         _grammar.InvokeRule(1, new[] { "Something", "To", "Eat" });
      }

      [Test]
      public void InvokingSimpleRuleWithComplexNestedRuleShouldSucceed() {
         int f1 = 0, f2 = 0;

         _grammar.AddRule("outer", e => e
            .Say("Something").OptionallyWithRule("inner").Say("To").Say("Eat")
               .Do(() => f2++)
         );
         _grammar.AddRule("inner", e => e
            .SayOneOf("Good", "Awesome", "Great")
               .Do(() => f1++)
            .Optionally(o => o.Say("Is").Say("Nice"))
         );

         _grammar.InvokeRule(1, new[] { "Something", "Awesome", "To", "Eat" });
         _grammar.InvokeRule(1, new[] { "Something", "Great", "Is", "Nice", "To", "Eat" });
         _grammar.InvokeRule(1, new[] { "Something", "To", "Eat" });

         Assert.That(f1, Is.EqualTo(2));
         Assert.That(f2, Is.EqualTo(3));
      }

   }
}
