// Project Renfrew
// Copyright(C) 2017  Stephen Workman (workman.stephen@gmail.com)
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

#define ISrGramCommonGUID "e8c3e160-c743-11cd-80e5-00aa003e4b50"

namespace Renfrew::NatSpeakInterop::Dragon::ComInterfaces {
   using namespace System::Runtime::InteropServices;

   [ComImport, Guid(ISrGramCommonGUID)]
   [InterfaceType(ComInterfaceType::InterfaceIsIUnknown)]
   public interface class
      DECLSPEC_UUID(ISrGramCommonGUID) ISrGramCommon {

      void Activate(HWND, BOOL, PCWSTR);
      void Archive(BOOL, PVOID, DWORD, DWORD *);
      void BookMark(QWORD, DWORD);
      void Deactivate(PCWSTR);
      void DeteriorationGet(DWORD *, DWORD *, DWORD *);
      void DeteriorationSet(DWORD, DWORD, DWORD);
      void TrainDlg(HWND, PCWSTR);
      void TrainPhrase(DWORD, PSDATA);
      void TrainQuery(DWORD *);
   };
}