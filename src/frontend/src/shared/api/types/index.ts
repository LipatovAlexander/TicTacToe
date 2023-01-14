export type ApiResponseErrors = string[]

export interface ApiResponse<TSuccess = undefined> {
    data: TSuccess
    isSuccessful: boolean
    errors: ApiResponseErrors
}
