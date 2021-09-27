// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

struct GetMerchandiseParams
{
    void* Func;
    void* RewardManager;
    int Level;
    int Index;
};

unsigned int WINAPI GetMerchandise(const GetMerchandiseParams & params)
{
    void* Func = params.Func;
    void* RewardManager = params.RewardManager;
    int Level = params.Level;
    int Index = params.Index;
    __asm
    {
        mov eax, Level
        mov edx, RewardManager
        mov ecx, Index
        call Func
    }
}

struct LookupStringParams
{
    void* Func;
    char* Name;
};

const wchar_t* WINAPI LookupString(const LookupStringParams& params)
{
    void* Func = params.Func;
    char* Name = params.Name;
    __asm
    {
        mov edx, Name
        call Func
    }
}