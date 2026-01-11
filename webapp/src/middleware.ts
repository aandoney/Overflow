import {auth} from "@/auth";
import {NextResponse} from "next/dist/server/web/spec-extension/response";

export default auth((req) => {
    if (req.auth) return NextResponse.next();
    
    const {nextUrl} = req;
    const callbackUrl = encodeURIComponent(nextUrl.pathname + nextUrl.search);
    return NextResponse.redirect(new URL(`/auth-gate?callbackUrl=${callbackUrl}`, nextUrl));
});

export const config = {
    matcher: [
        '/questions/ask',
        '/questions/:id/edit',
        '/session'
    ]
}