export interface UserRegisterModeldto {
    companyName: string | '';
    firstName: string | '';
    lastName: string | '';
    email: string | '';
    password: string | '';
    companyId: string | null;
    roleId: string | null;
    telephoneNumber: string | null;
    address: string | null;
    companyEmail: string | null;
    currencyId: number | null;
    isTrackInventory: boolean | false;
}
