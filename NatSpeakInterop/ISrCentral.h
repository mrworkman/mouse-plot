// Project Renfrew
// Copyright(C) 2016  Stephen Workman (workman.stephen@gmail.com)
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

#pragma once

#define ISrCentralGUID "b9bd3860-44db-101b-90a8-00aa003e4b50"

namespace Renfrew::NatSpeakInterop::Dragon::ComInterfaces {

   [ComImport, Guid(ISrCentralGUID)]
   [InterfaceType(ComInterfaceType::InterfaceIsIUnknown)]
   public interface class 
      DECLSPEC_UUID(ISrCentralGUID) ISrCentral {

      void ModeGet(PSRMODEINFOW);
      void GrammarLoad(SRGRMFMT, SDATA, IntPtr, IID, LPUNKNOWN *);
      void Pause();
      void PosnGet(PQWORD);
      void Resume();
      void ToFileTime(PQWORD, ::FILETIME *);
      void Register(IntPtr, IID, DWORD*);
      void UnRegister(DWORD);
   };
}