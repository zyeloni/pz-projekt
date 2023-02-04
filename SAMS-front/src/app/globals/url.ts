import { environment } from './../../environments/environment';
export const apiUrl = environment.apiUrl;

export const apiLogin = apiUrl+"users/login";
export const apiRegister = apiUrl+"users/register";
export const apiRefresh = apiUrl+"users/refresh";

export const apiGetAids = apiUrl+"aids/";
export const apiAddAid = apiGetAids+"add";
export const apiGetAllAids = apiGetAids+"getall";
export const apiPrintAids = apiGetAids+"print";

export const apiGetClubs = apiUrl+"clubs/get";
export const apiAddClub = apiUrl+"clubs/add";
export const apiLeaveClub = apiUrl+"clubs/leave/";
export const apiJoinClub = apiUrl+"clubs/join/";